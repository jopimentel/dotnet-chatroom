import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { IStockQuoteRequest } from '../../stock-quote-request';
import { ICorrelation } from '../entities/correlation.model';

@Injectable({ providedIn: 'root' })
export class BotService {

    constructor(private readonly httpClient: HttpClient) { }

    public requestQuote(request: IStockQuoteRequest, audience: string): Observable<ICorrelation> {
        const url: string = environment.endpoints.requests;
        const endpoint: string = url.replace('{stockCode}', request.stockCode)

        return this.httpClient.post<ICorrelation>(endpoint, request, { headers: { Audience: audience } });
    }
}