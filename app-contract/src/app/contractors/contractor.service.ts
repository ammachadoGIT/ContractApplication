import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { Contractor } from './contractor';
import { MessageService } from '../message.service';
import { BaseService } from '../shared/BaseService';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class ContractorService extends BaseService {

  private apiUrl = `${environment.apiUri}/api/contractors`;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient,
    messageService: MessageService) {
    super(messageService);
  }

  list(): Observable<Contractor[]> {
    return this.http.get<Contractor[]>(this.apiUrl)
      .pipe(
        tap(_ => this.log('fetched contractors')),
        catchError(this.handleError<Contractor[]>('getContractors', []))
      );
  }

  getById(id: number): Observable<Contractor> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Contractor>(url).pipe(
      tap(_ => this.log(`fetched contractor id=${id}`)),
      catchError(this.handleError<Contractor>(`getContractor id=${id}`))
    );
  }

  create(contractor: Contractor): Observable<Contractor> {
    return this.http.post<Contractor>(this.apiUrl, contractor, this.httpOptions).pipe(
      tap((newContractor: Contractor) => this.log(`added contractor w/ id=${newContractor.id}`)),
      catchError(this.handleError<Contractor>('addContractor'))
    );
  }

  getShortestPath(id1: number, id2: number): Observable<string> {
    const queryString = `fromId=${id1}&toId=${id2}`;
    return this.http.get<string>(`${this.apiUrl}/shortest-path?${queryString}`, this.httpOptions)
      .pipe(catchError(this.handleError<string>('addContractor')));
  }
}
