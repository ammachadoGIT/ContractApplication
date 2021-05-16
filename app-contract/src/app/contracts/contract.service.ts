import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Contract } from './contract';
import { MessageService } from '../shared/message.service';
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
        catchError(this.handleError<Contract[]>('list', []))
      );
  }

  getById(id1: number, id2: number): Observable<Contract> {
    const url = `${this.contractsUrl}/${id1}/${id2}`;
    return this.http.get<Contract>(url).pipe(
      catchError(this.handleError<Contract>(`getById id=${id1}/${id2}`))
    );
  }

  create(contract: Contract): Observable<Contract> {
    return this.http.post<Contract>(this.contractsUrl, contract, this.httpOptions).pipe(
      catchError(this.handleError<Contract>())
    );
  }

  delete(contractor1Id: number, contractor2Id: number): Observable<Contract> {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: {
        contractor1Id, contractor2Id
      },
    };

    return this.http.delete<Contract>(this.contractsUrl, options).pipe(
      catchError(this.handleError<Contract>('delete')));
  }

  getMessages(): string[] {
    return this.messageService.messages;
  }
}
