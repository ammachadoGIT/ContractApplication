import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { MessageService } from '../message.service';
import { BaseService } from '../shared/BaseService';
import { environment } from 'src/environments/environment';
import { EnumDescription } from '../contractors/EnumDescription';

@Injectable({ providedIn: 'root' })
export class EnumService extends BaseService {

  private enumsUrl = `${environment.apiUri}/api/enums`;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient,
    messageService: MessageService) {
    super(messageService);
  }

  listContractorType(): Observable<EnumDescription[]> {
    return this.http.get<EnumDescription[]>(this.enumsUrl + '/contractor-type')
      .pipe(
        catchError(this.handleError<EnumDescription[]>('listContractorType', []))
      );
  }

  listHealthStatus(): Observable<EnumDescription[]> {
    return this.http.get<EnumDescription[]>(this.enumsUrl + '/health-status')
      .pipe(
        catchError(this.handleError<EnumDescription[]>('listHealthStatus', []))
      );
  }
}
