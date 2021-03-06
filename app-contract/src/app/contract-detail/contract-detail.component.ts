import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Contract } from '../contracts/contract';
import { ContractService } from '../contracts/contract.service';
import { Contractor } from '../contractors/contractor';
import { ContractorService } from '../contractors/contractor.service';

@Component({
  selector: 'app-contract-detail',
  templateUrl: './contract-detail.component.html',
  styleUrls: ['./contract-detail.component.css']
})
export class ContractDetailComponent implements OnInit {
  contract: Contract;
  contractors: Contractor[];
  isReadOnly: boolean;
  validationMessages: string[];

  constructor(
    private route: ActivatedRoute,
    private contractService: ContractService,
    private contractorService: ContractorService,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.clearContract();
    this.listContractors();

    const id1 = +this.route.snapshot.paramMap.get('id1');
    const id2 = +this.route.snapshot.paramMap.get('id2');

    if (id1 && id2) {
      this.getById(id1, id2);
      this.isReadOnly = true;
    }
    else {
      this.isReadOnly = false;
    }
  }

  create(contract: Contract): void {
    this.contractService.messageService.clear();

    if (!contract || !contract.contractor1Id || !contract.contractor2Id) { return; }
    this.contractService.create(contract)
      .subscribe(_ => {
        this.validationMessages = this.contractService.messageService.messages;
        this.clearContract();
      });
  }

  getById(id1: number, id2: number): void {
    this.contractService.getById(id1, id2)
      .subscribe(contract => this.contract = contract);
  }

  goBack(): void {
    this.clearContract();
    this.location.back();
  }

  clearContract(): void {
    this.contract = {
      contractor1Id: null,
      contractor2Id: null,
      contractor2Name: '',
      contractor1Name: ''
    };
  }

  listContractors(): void {
    this.contractorService.list()
      .subscribe(contractors => this.contractors = contractors);
  }

  delete(contractor1Id: number, contractor2Id: number): void {
    this.contractService.delete(contractor1Id, contractor2Id)
      .subscribe(_ => this.goBack());
  }
}
