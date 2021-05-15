import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Contract } from './contract';
import { MessageService } from '../message.service';
import { BaseService } from '../shared/BaseService';
import { environment } from 'src/environments/environment';

@Injectable({ providedIn: 'root' })
export class ContractService extends BaseService {

  private contractsUrl = `${environment.apiUri}/api/contracts`;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient,
    messageService: MessageService) {
    super(messageService);
  }

  getContracts(): Observable<Contract[]> {
    return this.http.get<Contract[]>(this.contractsUrl)
      .pipe(
        tap(_ => this.log('fetched contracts')),
        catchError(this.handleError<Contract[]>('getContracts', []))
      );
  }

  getContractNo404<Data>(id: number): Observable<Contract> {
    const url = `${this.contractsUrl}/?id=${id}`;
    return this.http.get<Contract[]>(url)
      .pipe(
        map(contracts => contracts[0]),
        tap(h => {
          const outcome = h ? `fetched` : `did not find`;
          this.log(`${outcome} contract id=${id}`);
        }),
        catchError(this.handleError<Contract>(`getContract id=${id}`))
      );
  }

  getContract(id: number): Observable<Contract> {
    const url = `${this.contractsUrl}/${id}`;
    return this.http.get<Contract>(url).pipe(
      tap(_ => this.log(`fetched contract id=${id}`)),
      catchError(this.handleError<Contract>(`getContract id=${id}`))
    );
  }

  addContract(contract: Contract): Observable<Contract> {
    return this.http.post<Contract>(this.contractsUrl, contract, this.httpOptions).pipe(
      tap((newContract: Contract) => this.log(`added contract ${newContract.contractor1Id}--${newContract.contractor2Id}`)),
      catchError(this.handleError<Contract>('addContract'))
    );
  }
}
