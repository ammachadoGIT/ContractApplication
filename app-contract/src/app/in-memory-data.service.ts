import { Injectable } from '@angular/core';
import { InMemoryDbService } from 'angular-in-memory-web-api';
import { Contractor } from './contractors/contractor';

@Injectable({
  providedIn: 'root',
})
export class InMemoryDataService implements InMemoryDbService {
  createDb() {
    const contractors = [
      { id: 1, name: 'Dr Nice', address: '88 Isabella St', phoneNumber: '11', contractorType: 0, healthStatus: 1 },
      { id: 2, name: 'Narco', address: '88 Isabella St', phoneNumber: '11', contractorType: 1, healthStatus: 0 },
      { id: 3, name: 'Bombasto', address: '88 Isabella St', phoneNumber: '11', contractorType: 2, healthStatus: 2 },
      { id: 4, name: 'Celeritas', address: '88 Isabella St', phoneNumber: '11', contractorType: 1, healthStatus: 1 },
      { id: 5, name: 'Magneta', address: '88 Isabella St', phoneNumber: '11', contractorType: 0, healthStatus: 0 }
    ];
    const contracts = [
      { contractor1Id: 1, contractor2Id: 2 },
    ];
    return { contractors, contracts };
  }

  genId(contractors: Contractor[]): number {
    return contractors.length > 0 ? Math.max(...contractors.map(contractor => contractor.id)) + 1 : 1;
  }
}
