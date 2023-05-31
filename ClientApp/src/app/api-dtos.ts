export interface UserDto{
  username: string;
  email: string;
  creationDate: Date;
}

export interface NoteDto{
  id: number;
  name: string;
  content: string;
}

export interface CategoryDto{
  id: number;
  name: string;
}

export interface TokenDto {
  token: string;
}
