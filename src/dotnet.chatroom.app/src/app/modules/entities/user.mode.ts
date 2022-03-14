import { IChat } from './chat.model';

export interface IUser {
    id: string;
    username: string;
    name: string;
    email: string;
    password: string;
    created: Date;

    chats?: Array<IChat>;
}