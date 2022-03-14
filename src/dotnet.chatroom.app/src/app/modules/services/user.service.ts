import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { IChat } from '../entities/chat.model';
import { IUser } from '../entities/user.mode';

@Injectable({ providedIn: 'root' })
export class UserService {

    constructor(private readonly httpClient: HttpClient) { }

    public getChats(id: string): Observable<Array<IChat>> {
        const url: string = environment.endpoints.chats;
        const endpoint: string = url.replace('{id}', id);

        return this.httpClient.get<Array<IChat>>(endpoint);
    }

    public getById(id: string): Observable<IUser> {
        const url: string = environment.endpoints.user;
        const endpoint: string = url.replace('{id}', id);

        return this.httpClient.get<IUser>(endpoint);
    }
}