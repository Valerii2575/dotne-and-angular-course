import { Photo } from "./photo";

export interface Member {
  id: number;
  userName: string;
  age: number;
  photoUrl: string;
  knownAs: string;
  created: Date;
  lastActive: Date;
  gemder: string;
  introduction: string;
  interests: string;
  lookingFor: string;
  country: string;
  photos: Photo[];
}


