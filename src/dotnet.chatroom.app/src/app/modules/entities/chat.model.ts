import { ChatType } from '../enums/chat-type.enum';
import { IUser } from './user.mode';

export interface IChat {
    id: string;
    name: string;
    type: ChatType;
    created: Date;

    users?: Array<IUser>
}