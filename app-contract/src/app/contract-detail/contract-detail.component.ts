import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Contract } from '../contracts/contract';
import { ContractService } from '../contracts/contract.service';

@Component({
  selector: 'app-contract-detail',
  templateUrl: './contract-detail.component.html',
  styleUrls: ['./contract-detail.component.css']
})
export class ContractDetailComponent implements OnInit {
  contract: Contract;
  isReadOnly: boolean;

  constructor(
    private route: ActivatedRoute,
    private contractService: ContractService,
    private location: Location
  ) { }

  ngOnInit(): void {
    const id1 = +this.route.snapshot.paramMap.get('id1');
    const id2 = +this.route.snapshot.paramMap.get('id2');

    if (id1 && id2) {
      this.getById(id1, id2);
      this.isReadOnly = true;
    }
    else {
      this.clearContract();
      this.isReadOnly = false;
    }
  }

  create(contract): void {
    // TODO:
    if (false) { return; }
    this.contractService.create(contract)
      .subscribe(_ => this.clearContract);
  }

  getById(id1: number, id2: number): void {
    this.contractService.getById(id1, id2)
      .subscribe(contract => this.contract = contract);
  }

  goBack(): void {
    this.location.back();
  }

  clearContract(): void {
    this.contract = {
      contractor1Id: null,
      contractor2Id: null
    };
  }
}
