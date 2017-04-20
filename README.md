Basic demo xamarin forms app base - actual demo apps/functions will be built into or forked from this.

## ListView Item Selection - Change its background colour:

See [ListViewDemoPage](https://github.com/wislon/xfdemos/blob/master/src/xamformsdemo/xamformsdemo/ListViewDemoPage.xaml) example.  I also wrote a [short blog post about it](http://blog.wislon.io/posts/2017/04/11/xamforms-listview-selected-colour).

_(Apologies for the janky iOS gifs, they were done from the simulator running over a long-distance wifi connection)_

### iOS implementation
Before implementing on iOS. Note that you can't tell when an item is selected.

![iOS - Before implementation](https://github.com/wislon/xfdemos/blob/master/screenshots/listviewcolor/ios-before.gif)

After implementing on iOS (change selected colour to teal)

![iOS - After implementation](https://github.com/wislon/xfdemos/blob/master/screenshots/listviewcolor/ios-after.gif)

### Android implementation
Before implementing on Android (ugh orange, from the default theme)

![Android - Before implementation](https://github.com/wislon/xfdemos/blob/master/screenshots/listviewcolor/droid-before.gif)

After implementing on Android (better, teal)

![Android - After implementation](https://github.com/wislon/xfdemos/blob/master/screenshots/listviewcolor/droid-after.gif)

