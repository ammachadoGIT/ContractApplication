import { Component, OnInit } from '@angular/core';

import { Contractor } from './contractor';
import { ContractorService } from './contractor.service';

@Component({
  selector: 'app-contractors',
  templateUrl: './contractors.component.html',
  styleUrls: ['./contractors.component.css']
})
export class ContractorsComponent implements OnInit {
  contractors: Contractor[];

  constructor(private contractorService: ContractorService) { }

  ngOnInit() {
    this.getContractors();
  }

  getContractors(): void {
    this.contractorService.getContractors()
      .subscribe(contractors => this.contractors = contractors);
  }

  goBack(): void {
    // this.location.back();
  }
}
