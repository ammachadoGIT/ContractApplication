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
  counter: 0;

  constructor(private contractService: ContractService) { }

  ngOnInit() {
    this.getContracts();
  }

  getContracts(): void {
    this.contractService.getContracts()
      .subscribe(contracts => this.contracts = contracts);
  }

  add(contractor1Id: number, contractor2Id: number): void {
    // TODO:
    if (false) { return; }
    this.contractService.addContract({ contractor1Id, contractor2Id } as Contract)
      .subscribe(contract => {
        this.contracts.push(contract);
      });
  }
}
