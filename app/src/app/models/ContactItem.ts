import {StudyCourse} from 'src/app/models/StudyCourse';
import {UserType} from 'src/app/models/UserType';

export interface ContactItem {
    contactID: number;
    firstName: string;
    lastName: string;
    title: string;
    telNumber: string;
    email: string;
    room: string;
    responsibility: string;
    course: StudyCourse;
    type: UserType;
}
