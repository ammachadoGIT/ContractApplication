import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';

import { Contractor } from '../contractors/contractor';
import { ContractorService } from '../contractors/contractor.service';

@Component({
  selector: 'app-shortest-path',
  templateUrl: './shortest-path.component.html',
  styleUrls: ['./shortest-path.component.css']
})
export class ShortestPathComponent implements OnInit {
  shortestPathMessage: string;
  contractors: Contractor[];

  constructor(
    private contractorService: ContractorService,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.listContractors();
  }

  getShortestPath(id1: number, id2: number): void {
    this.contractorService.getShortestPath(id1, id2)
      .subscribe(result => this.shortestPathMessage = result.length > 0 
         ? 'The shortest path is: ' +result.map(x => `${x.name} (${x.id})`).join(' -- ') 
         : 'No path was found between the contractors.');
  }

  goBack(): void {
    this.location.back();
  }

  listContractors(): void {
    this.contractorService.list()
      .subscribe(contractors => this.contractors = contractors
        );
  }
}
