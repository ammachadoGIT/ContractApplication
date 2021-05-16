import { Component, OnInit } from '@angular/core';

import { Contract } from './contract';
import { ContractService } from './contract.service';

@Component({
  selector: 'app-contracts',
  templateUrl: './contracts.component.html',
  styleUrls: ['./contracts.component.css']
})
export class ContractsComponent implements OnInit {
  contracts: Contract[];

  constructor(private contractService: ContractService) { }

  ngOnInit() {
    this.list();
  }

  list(): void {
    this.contractService.list()
      .subscribe(contracts => this.contracts = contracts);
  }
}
