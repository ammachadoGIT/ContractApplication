import { MessageService } from './message.service';
import { Observable, of } from 'rxjs';

export class BaseService {

  constructor(
    public messageService: MessageService) { }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  protected handleError<T>(operation = 'Operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error); // log to console instead
      this.log(`${operation} failed: ${error.error || error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result);
    };
  }

  /** Log a ContractorService message with the MessageService */
  protected log(message: string) {
    this.messageService.add(message);
  }
}
