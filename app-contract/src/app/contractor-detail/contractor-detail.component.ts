import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Contractor } from '../contractors/contractor';
import { ContractorService } from '../contractors/contractor.service';

@Component({
  selector: 'app-contractor-detail',
  templateUrl: './contractor-detail.component.html',
  styleUrls: ['./contractor-detail.component.css']
})
export class ContractorDetailComponent implements OnInit {
  contractor: Contractor;
  isReadOnly: boolean;

  constructor(
    private route: ActivatedRoute,
    private contractorService: ContractorService,
    private location: Location
  ) { }

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    if (id) {
      this.getContractor(id);
      this.isReadOnly = true;
    }
    else {
      this.clearContractor();
      this.isReadOnly = false;
    }
  }

  add(contractor: Contractor): void {
    this.contractorService.addContractor(contractor)
      .subscribe(_ => this.clearContractor());
  }

  getContractor(id: number): void {
    this.contractorService.getContractor(id)
      .subscribe(contractor => this.contractor = contractor);
  }

  goBack(): void {
    this.location.back();
  }

  clearContractor() {
    this.contractor = {
      id: 0,
      name: '',
      address: '',
      phoneNumber: '',
      healthStatus: null,
      contractorType: null
    };
  }
}
