import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Contractor } from '../contractors/contractor';
import { ContractorService } from '../contractors/contractor.service';
import { EnumService } from '../shared/EnumService';
import { EnumDescription } from '../contractors/EnumDescription';

@Component({
  selector: 'app-contractor-detail',
  templateUrl: './contractor-detail.component.html',
  styleUrls: ['./contractor-detail.component.css']
})
export class ContractorDetailComponent implements OnInit {
  contractor: Contractor;
  isReadOnly: boolean;
  healthStatuses: EnumDescription[];
  contractorTypes: EnumDescription[];

  constructor(
    private route: ActivatedRoute,
    private contractorService: ContractorService,
    private enumService: EnumService,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.clearContractor();
    this.listHealthStatus();
    this.listContractorType();

    const id = +this.route.snapshot.paramMap.get('id');
    if (id) {
      this.getContractor(id);
      this.isReadOnly = true;
    }
    else {
      this.isReadOnly = false;
    }
  }

  add(contractor: Contractor): void {
    this.contractorService.create(contractor)
      .subscribe(_ => this.clearContractor());
  }

  getContractor(id: number): void {
    this.contractorService.getById(id)
      .subscribe(contractor => this.contractor = contractor);
  }

  goBack(): void {
    this.clearContractor();
    this.location.back();
  }

  clearContractor() {
    this.contractor = {
      id: 0,
      name: '',
      address: '',
      phoneNumber: '',
      healthStatus: null,
      type: null
    };
  }

  listHealthStatus(): void {
    this.enumService.listHealthStatus()
      .subscribe(statuses => this.healthStatuses = statuses);
  }

  listContractorType(): void {
    this.enumService.listContractorType()
      .subscribe(types => this.contractorTypes = types);
  }
}
