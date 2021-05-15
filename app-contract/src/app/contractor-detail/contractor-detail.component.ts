import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Contractor } from '../contractors/contractor';
import { ContractorService } from '../contractors/contractor.service';

@Component({
  selector: 'app-contractor-detail',
  templateUrl: './contractor-detail.component.html',
  styleUrls: [ './contractor-detail.component.css' ]
})
export class ContractorDetailComponent implements OnInit {
  contractor: Contractor;

  constructor(
    private route: ActivatedRoute,
    private contractorService: ContractorService,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.getContractor();
  }

  getContractor(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.contractorService.getContractor(id)
      .subscribe(contractor => this.contractor = contractor);
  }

  goBack(): void {
    this.location.back();
  }
}
