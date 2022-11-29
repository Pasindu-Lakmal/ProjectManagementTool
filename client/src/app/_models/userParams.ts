import { User } from './user';

export class UserParams {
  //inside class we can use constructor and intialie the values

  gender: string;
  minAge = 18;
  maxAge = 99;
  pageNumber = 1;
  pageSize = 2;
  orderBy = 'lastActive';

  constructor(user: User) {
    this.gender = user.gender === 'female' ? 'male' : 'female';
  }
}
