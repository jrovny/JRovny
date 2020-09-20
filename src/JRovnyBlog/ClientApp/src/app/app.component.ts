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
    },
    {
      postId: 2,
      title: "Test 2",
      content: "Test 2",
    },
  ];
}
