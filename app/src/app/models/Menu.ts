import { Meal } from "./Meal";

export interface Menu {
    menuID: number;
    date: Date;
    meal: Meal;
    price: number;
}