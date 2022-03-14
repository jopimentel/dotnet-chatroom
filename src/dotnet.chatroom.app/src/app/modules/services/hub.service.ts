import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class HubService {

    private retryIntervals: Array<number> = [10000, 15000, 20000, 25000, 30000, 35000, 40000, 45000, 50000, 55000, 60000];
    private baseUrl: string = environment.endpoints.hubs;

    public buildHubConnection(hub: string): HubConnection {
        return new HubConnectionBuilder()
            .withUrl(`${this.baseUrl}/${hub}`)
            .configureLogging(LogLevel.Information)
            .withAutomaticReconnect(this.retryIntervals)
            .build();
    }
}