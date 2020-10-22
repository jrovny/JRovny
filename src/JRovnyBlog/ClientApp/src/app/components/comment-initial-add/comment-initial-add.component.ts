import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { select, Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { InitialComment } from 'src/app/models/initial-comment';
import { CommentService } from 'src/app/services/comment.service';
import { selectCurrentPostId } from 'src/app/store/reducers';
import { State } from 'src/app/store/reducers/app.reducer';

@Component({
  selector: 'app-comment-initial-add',
  templateUrl: './comment-initial-add.component.html',
  styleUrls: ['./comment-initial-add.component.scss'],
})
export class CommentInitialAddComponent implements OnInit, OnDestroy {
  form = this.fb.group({
    content: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    name: ['', Validators.required],
    website: [''],
    rememberMe: [false, Validators.required],
  });
  subscription$: Subscription;
  @ViewChild('formDirective') private formDirective: NgForm;
  postId: number;

  constructor(
    private fb: FormBuilder,
    private commentService: CommentService,
    private store: Store<State>
  ) {}

  ngOnInit(): void {
    this.store.pipe(select(selectCurrentPostId)).subscribe((id) => {
      this.postId = id;
    });
  }

  onSubmit() {
    const model = this.form.value;
    const comment: InitialComment = {
      content: model.content,
      email: model.email,
      name: model.name,
      website: model.website,
      rememberMe: model.rememberMe,
    };
    this.subscription$ = this.commentService
      .createInitialComment(this.postId, comment)
      .subscribe(() => {
        this.formDirective.resetForm();
      });
  }

  ngOnDestroy(): void {
    if (this.subscription$) {
      this.subscription$.unsubscribe();
    }
  }
}
