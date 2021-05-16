using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContractApplication.Models;
using ContractApplication.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContractApplication.Services
{
    public class ContractorService : ServiceBase, IContractorService
    {
        private readonly ApplicationDbContext context;

        public ContractorService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<ContractorDto> ListContractors()
        {
            var contractors = this.context.Contractors.Include(c => c.ContractFrom).Include(c => c.ContractTo).ToList();
            return this.Mapper.Map<IEnumerable<ContractorDto>>(contractors);
        }

        public async Task<ContractorDto> GetContractorByIdAsync(int id)
        {
            var contractor = await this.context.Contractors.FindAsync(id);
            return this.Mapper.Map<ContractorDto>(contractor);
        }

        public async Task<ContractorDto> CreateContractor(ContractorDto contractorDto)
        {
            var contractor = this.Mapper.Map<Contractor>(contractorDto);
            contractor.AssignHealthStatus();

            await this.context.Contractors.AddAsync(contractor);
            await this.context.SaveChangesAsync();

            return this.Mapper.Map<ContractorDto>(contractor);
        }

        public List<ContractorDto> GetShortestPath(
            int fromContractor,
            int toContractor,
            IEnumerable<ContractorDto> contractorDtos)
        {
            var previousNodeDictionary = new Dictionary<ContractorDto, ContractorDto>();
            var visitedHashSet = new HashSet<ContractorDto>();
            var queue = new Queue<ContractorDto>();

            queue.Enqueue(contractorDtos.First(x => x.Id == fromContractor));

            while (queue.Count > 0)
            {
                var currentDto = queue.Dequeue();
                if (visitedHashSet.Contains(currentDto))
                {
                    continue;
                }

                visitedHashSet.Add(currentDto);

                foreach (var neighborNode in currentDto.ContractFrom)
                {
                    var neighborContractor = neighborNode.Contractor2;
                    if (FindById(neighborContractor.Id, previousNodeDictionary) != null)
                    {
                        continue;
                    }

                    previousNodeDictionary.Add(neighborContractor, currentDto);
                    queue.Enqueue(neighborContractor);
                }

                foreach (var neighborNode in currentDto.ContractTo)
                {
                    var neighborContractor = neighborNode.Contractor1;
                    if (FindById(neighborContractor.Id, previousNodeDictionary) != null)
                    {
                        continue;
                    }

                    previousNodeDictionary.Add(neighborContractor, currentDto);
                    queue.Enqueue(neighborContractor);
                }
            }

            return PrintShortestPath(
                FindById(fromContractor, previousNodeDictionary),
                FindById(toContractor, previousNodeDictionary),
                previousNodeDictionary);
        }

        private static List<ContractorDto> PrintShortestPath(
            ContractorDto fromContractor,
            ContractorDto toContractor,
            IReadOnlyDictionary<ContractorDto, ContractorDto> nodeDictionary)
        {
            var result = new List<ContractorDto>();
            if (toContractor == null)
            {
                return new List<ContractorDto>();
            }

            var currentNode = toContractor;
            while (currentNode != fromContractor)
            {
                result.Add(currentNode);
                currentNode = nodeDictionary[currentNode];
            }

            result.Add(fromContractor);
            result.Reverse();
            return result;
        }

        private static ContractorDto FindById(int id, IReadOnlyDictionary<ContractorDto, ContractorDto> nodeDictionary)
        {
            return nodeDictionary.Keys.FirstOrDefault(x => x.Id == id);
        }
    }
}