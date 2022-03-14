import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { IChat } from '../entities/chat.model';
import { IMessage } from '../entities/message.model';

@Injectable({ providedIn: 'root' })
export class ChatService {

    private baseUrl: string = environment.endpoints.chat;

    constructor(private readonly httpClient: HttpClient) { }

    public add(chat: IChat): Observable<any> {
        return this.httpClient.post(this.baseUrl, chat)
    }

    public getByAudience(audience: string): Observable<Array<IMessage<any>>> {
        const url: string = environment.endpoints.messages;
        const endpoint: string = url.replace('{audience}', audience);

        return this.httpClient.get<Array<IMessage<any>>>(endpoint);
    }
}