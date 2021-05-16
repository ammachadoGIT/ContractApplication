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

  list(): Observable<Contract[]> {
    return this.http.get<Contract[]>(this.contractsUrl)
      .pipe(
        tap(_ => this.log('fetched contracts')),
        catchError(this.handleError<Contract[]>('list', []))
      );
  }

  getById(id1: number, id2: number): Observable<Contract> {
    const url = `${this.contractsUrl}/${id1}/${id2}`;
    return this.http.get<Contract>(url).pipe(
      tap(_ => this.log(`fetched contract id=${id1}/${id2}`)),
      catchError(this.handleError<Contract>(`getById id=${id1}/${id2}`))
    );
  }

  create(contract: Contract): Observable<Contract> {
    return this.http.post<Contract>(this.contractsUrl, contract, this.httpOptions).pipe(
      tap((newContract: Contract) => this.log(`added contract ${newContract.contractor1Id}--${newContract.contractor2Id}`)),
      catchError(this.handleError<Contract>('create'))
    );
  }
}
