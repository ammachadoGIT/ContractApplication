import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ContractorsComponent } from './contractors/contractors.component';
import { ContractorDetailComponent } from './contractor-detail/contractor-detail.component';
import { ContractsComponent } from './contracts/contracts.component';
import { ContractDetailComponent } from './contract-detail/contract-detail.component';
import { ShortestPathComponent } from './shortest-path/shortest-path.component';

const routes: Routes = [
  { path: '', redirectTo: '/contractors', pathMatch: 'full' },
  { path: 'contractors', component: ContractorsComponent },
  { path: 'contractors/new', component: ContractorDetailComponent },
  { path: 'contractors/shortest-path', component: ShortestPathComponent },
  { path: 'contractors/:id', component: ContractorDetailComponent },
  { path: 'contracts', component: ContractsComponent },
  { path: 'contracts/new', component: ContractDetailComponent },
  { path: 'contracts/:id1/:id2', component: ContractDetailComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
