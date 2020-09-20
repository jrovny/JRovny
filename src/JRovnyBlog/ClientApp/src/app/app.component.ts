import { Component } from "@angular/core";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
})
export class AppComponent {
  title = "JRovny Blog";
  blogPosts = [
    {
      postId: 1,
      title: "Test",
      content: "Test",
      createdDate: "2020-09-20T14:24:53.808975-05:00",
    },
    {
      postId: 2,
      title: "Test 2",
      content: "Test 2",
      createdDate: "2020-09-20T14:24:53.808975-05:00",
    },
  ];
}
