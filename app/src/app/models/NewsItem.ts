import { PostGroup } from "./PostGroup";

export interface NewsItem {
    id: number;
    title: string;
    message: string;
    postGroup: PostGroup;
    date: Date;
    authorID: number;
}