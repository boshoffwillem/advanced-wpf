# advanced-wpf
A repository containing advanced WPF concepts


## 1.Visual States

In WPF, visual states are multiple appearances that your control can display, depending on any criteria you set.

We have a button control, that represents a clock. During day time it will have a light visual state, and during night time it will have a darker visual state.

## 2.MVVM

Model-View-ViewModel (MVVM) is an structural design pattern that separates an applications functionality into three parts. 
MVVM is equivalent to Model-View-Controller (MVC).
The three parts are:

### 2.1 Model

Models are the lowest layer of classes in this pattern. They contain all the business data and logic. 
If you have a Person table in a database, for example, you would create a Person model/class that contains all the data that's in the database (usually every field is represented by a property), and any logic than can be applied to a Person.

### 2.2 ViewModel

The layer above the Model is the ViewModel. ViewModels are simply classes that are responsible for converting Model data into formats that can be displayed and used by a View.

### 2.3 View

The highest layer is the View. The view does not know about business data or logic. It's should be as decoupled as possible from any domain logic or data.
The view will simply used data that is exposed by the ViewModel and then render it. The idea is that if you want to change the way you render or display your data, you only have to rewrite your Views, not your Models and ViewModels.

In the example in the repository we have a simple application that demonstrates the separation of data, as well as how to switch between different Views.