import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ContractorsComponent } from './contractors/contractors.component';
import { ContractorDetailComponent } from './contractor-detail/contractor-detail.component';
import { ContractsComponent } from './contracts/contracts.component';

const routes: Routes = [
  { path: '', redirectTo: '/contractors', pathMatch: 'full' },
  { path: 'contractors', component: ContractorsComponent },
  { path: 'contractors/:id', component: ContractorDetailComponent },
  { path: 'contracts', component: ContractsComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
