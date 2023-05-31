import {Component, Inject} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {CategoryDto, NoteDto} from "../api-dtos";

@Component({
  selector: 'app-panel',
  templateUrl: './panel.component.html'
})
export class PanelComponent {
  categories?: CategoryDto[];
  notes?: NoteDto[];
  selectedCategory?: CategoryDto;
  selectedNote?: NoteDto;

  addCategoryMenuVisible: boolean = false;
  addNoteMenuVisible: boolean = false;

  addCategoryMenuVisibleToggle() {
    this.addCategoryMenuVisible = !this.addCategoryMenuVisible;
  }

  addNoteMenuVisibleToggle() {
    this.addNoteMenuVisible = !this.addNoteMenuVisible;
  }

  addCategory() {
    const token = <string>window.localStorage.getItem("token");
    const name = (<HTMLInputElement>document.getElementById("category-name-input")).value;

    if (name.length < 1 || name.length > 32) {
      alert("Category name must be between 1 and 32 characters");
      return;
    }

    this.http.post(this.baseUrl + 'api/Category/CreateCategory', {token: token, name: name})
      .subscribe(next => {
          const category = next as CategoryDto;
          this.categories?.push(category);

          (<HTMLInputElement>document.getElementById("category-name-input")).value = "";
          this.addCategoryMenuVisibleToggle();

          alert("Category created");
        }
        , error => {
          const response = error as any;
          const value = response?.error?.text;

          //TODO: Add proper error handling
          alert(value);
        });
  }

  addNote() {
    const token = <string>window.localStorage.getItem("token");
    const name = (<HTMLInputElement>document.getElementById("note-name-input")).value;
    const content = (<HTMLInputElement>document.getElementById("note-content-input")).value;

    if (name.length < 1 || name.length > 32) {
      alert("Note name must be between 1 and 32 characters");
      return;
    }

    if (content.length < 1 || content.length > 2048) {
      alert("Note content must be between 1 and 2048 characters");
      return;
    }

    if (this.selectedCategory === undefined) {
      alert("Select category");
      return;
    }

    this.http.post(this.baseUrl + 'api/Note/CreateNote', {
      token: token,
      title: name,
      content: content,
      categoryId: this.selectedCategory.id
    })
      .subscribe(next => {
          const note = next as NoteDto;
          this.notes?.push(note);

          (<HTMLInputElement>document.getElementById("note-name-input")).value = "";
          (<HTMLInputElement>document.getElementById("note-content-input")).value = "";
          this.addNoteMenuVisibleToggle();

          alert("Note created");
        }
        , error => {
          const response = error as any;
          const value = response?.error?.text;

          //TODO: Add proper error handling
          alert(value);
        });
  }

  updateNote() {
    const token = <string>window.localStorage.getItem("token");
    const name = (<HTMLInputElement>document.getElementById("current-note-name-input")).value;
    const content = (<HTMLInputElement>document.getElementById("current-note-content-input")).value;

    if (name.length < 1 || name.length > 32) {
      alert("Note name must be between 1 and 32 characters");
      return;
    }

    if (content.length < 1 || content.length > 2048) {
      alert("Note content must be between 1 and 2048 characters");
      return;
    }

    if (this.selectedNote === undefined) {
      alert("Select note");
      return;
    }

    this.http.post(this.baseUrl + 'api/Note/UpdateNote', {
      token: token,
      title: name,
      content: content,
      id: this.selectedNote.id
    })
      .subscribe(next => {
        alert("Note updated");

        this.categorySelected({value: JSON.stringify(this.selectedCategory)});
      }, error => {
        const response = error as any;
        const value = response?.error?.text;

        //TODO: Add proper error handling
        alert(value);
      });
  }

  deleteNote() {
    //display confirmation dialog
    if (!confirm("Are you sure you want to delete this note?")) {
      return;
    }

    const token = <string>window.localStorage.getItem("token");

    if (this.selectedNote === undefined) {
      alert("Select note");
      return;
    }

    this.http.post(this.baseUrl + 'api/Note/DeleteNote', {
      token: token,
      id: this.selectedNote.id
    })
      .subscribe(next => {
        alert("Note deleted");

        this.categorySelected({value: JSON.stringify(this.selectedCategory)});
      }, error => {
        const response = error as any;
        const value = response?.error?.text;

        //TODO: Add proper error handling
        alert(value);
      });
  }

  deleteCategory() {
    //display confirmation dialog
    if (!confirm("Are you sure you want to delete this category?")) {
      return;
    }

    const token = <string>window.localStorage.getItem("token");

    if (this.selectedCategory === undefined) {
      alert("Select category");
      return;
    }

    this.http.post(this.baseUrl + 'api/Category/DeleteCategory', {
      token: token,
      id: this.selectedCategory.id
    })
      .subscribe(next => {
        alert("Category deleted");

        this.categories = undefined;
        this.notes = undefined;
        this.selectedCategory = undefined;
        this.selectedNote = undefined;
        this.ngOnInit();
      }, error => {
        const response = error as any;
        const value = response?.error?.text;

        //TODO: Add proper error handling
        alert(value);
      });
  }

  categorySelected(target: any) {
    this.selectedNote = undefined;

    const selection = target.value;

    if (selection === "null") {
      this.selectedCategory = undefined;
      return;
    }

    this.selectedCategory = JSON.parse(selection) as CategoryDto;

    this.http.post(this.baseUrl + 'api/Note/GetNotes', {
      token: window.localStorage.getItem("token"),
      categoryId: this.selectedCategory.id
    })
      .subscribe(next => {
          this.notes = next as NoteDto[];
        }
        , error => {
          const response = error as any;
          const value = response?.error?.text;

          //TODO: Add proper error handling
          alert(value);
        });
  }

  noteSelected(target: any) {
    const selection = target.value;

    if (selection === "null") {
      this.selectedNote = undefined;
      return;
    }

    this.selectedNote = JSON.parse(selection) as NoteDto;

    //wait for current-note-name-input and current-note-content-input to be created
    setTimeout(() => {
      (<HTMLInputElement>document.getElementById("current-note-name-input")).value = this.selectedNote!.name;
      (<HTMLInputElement>document.getElementById("current-note-content-input")).value = this.selectedNote!.content;
    }, 100);
  }

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  ngDoCheck() {
    if ((<HTMLInputElement>document.getElementById("current-note-name-input")) && (<HTMLInputElement>document.getElementById("current-note-content-input"))
      && this.selectedNote) {
      (<HTMLInputElement>document.getElementById("current-note-name-input")).value = this.selectedNote.name;
      (<HTMLInputElement>document.getElementById("current-note-content-input")).value = this.selectedNote.content;
    }
  }

  ngOnInit() {
    this.http.post(this.baseUrl + 'api/Category/GetCategories', {token: window.localStorage.getItem("token")})
      .subscribe(next => {
        this.categories = next as CategoryDto[];
      }, error => {
        const response = error as any;
        const status = response.status;
        const value = response?.error?.text;

        //TODO: Add proper error handling
        alert(value);
      });
  }

  toJson(obj: any) {
    return JSON.stringify(obj);
  }
}
