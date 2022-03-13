import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, OnDestroy } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { from, tap } from 'rxjs';
import { environment } from '../environments/environment';
import { IStockQuoteRequest } from './stock-quote-request';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements AfterViewInit, OnDestroy {

  public messages: Array<any> = [];
  public readonly connection: HubConnection = new HubConnectionBuilder()
    .withUrl('http://localhost:25594/hub/messages')
    .configureLogging(LogLevel.Information)
    .withAutomaticReconnect([10000, 15000, 20000, 25000, 30000, 35000, 40000, 45000, 50000, 55000, 60000])
    .build();

  constructor(private readonly httpClient: HttpClient) { }

  public ngAfterViewInit(): void {
    from(this.connection.start())
      .pipe(
        tap(() => this.subscribeToMessages())
      ).subscribe();
  }

  public ngOnDestroy(): void {
    this.connection.stop();
  }

  private subscribeToMessages(): void {
    this.connection.on('messages-1', message => {
      console.log(message);
      this.messages.push(message);
    });
  }

  public request(message: string): void {
    const command = environment.commands.filter(c => this.isCommand(message, c))[0];

    if (!command)
      console.log(`${message} isn't a valid command.`);

    const body: IStockQuoteRequest = {
      stockCode: message.replace(command, ''),
      action: command
    };
    const url: string = `http://localhost:8082/api/requests/${body.stockCode}`;

    this.httpClient.post(url, body)
      .pipe(
        tap(r => console.log(r))
      ).subscribe();
  }

  private isCommand(value: string, command: string): boolean {
    return new RegExp(`^${command}`).test(value);
  }
}