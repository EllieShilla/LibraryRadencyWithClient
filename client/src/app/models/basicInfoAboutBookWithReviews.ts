import { IGetReview } from './getReview';

export class BasicInfoAboutBookWithReview {
  id!: number;
  title!: string;
  author!: string;
  cover!: string;
  genre!: string;
  content!: string;
  rating!: number;
  reviews: IGetReview[] = [];
}
