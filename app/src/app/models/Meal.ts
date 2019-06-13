import {Place} from './Place';

export interface Meal {
    mealID: number;
    place: Place;
    mealName: string;
    description: string;
}
