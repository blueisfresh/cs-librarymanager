   M S B A M L   `   `   `  ����  LibraryManagement*    $LibraryManagement.Views.StudentsPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  Schülerverwaltung��5      $	��500��6!   $	��600��6.   .��5      � $��10q�6   .+�5   
   �� $��Schülerverwaltung��6   $
��Bold"�6-   $��20(�6?   $��Center=�6M   $	��Top=�6j   $��	0,10,0,20q�6�   �� 5
   
    �� Orientation$ 
Horizontal=�6   $
��Left=�6.   $	��Top=�6I   $��	10,50,0,0q�6a   .+�5      �� $��	Einfügen��6   $	��100��6(   $��0,0,10,0q�64   ��6F   �� *OpenAddStudentWindowCommand+5      �� 5   
   $S��MHinweis: Sie können nach den Nachnamen suchen, nicht nach der Ausweisnummer.��6   
�������$��Italic$�5   &   $��	10,80,0,0q�69   �� 5   
   $ 
Horizontal=�6   $	��Top=�6.   $��
10,110,0,0q�6F   .+�5      �� $��	Suchfeld:��6   $��Center=�6)   $��0,0,10,0q�6D   �� 5      $	��300��6   ��6"   �� *
SearchTerm+ �� UpdateSourceTrigger$ PropertyChanged=��� 5      $��Suchen��6   $��75��6&   $��10,0,0,0q�61   ��6C   �� *SearchCommand+5      &   System.Windows.Controls.DataGrid  5   
   $��10,150,10,10q�5         AutoGenerateColumns$ False��5         
IsReadOnly$
 True��5      ��5      �� *FilteredStudents+ �� SelectedItem 5      �� *SelectedStudent+ �� Mode$ TwoWay=�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn   Header$ Nachname��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 200 6T   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 67   �� *
LastName+6T     5!      $ Vorname��6%   $		 150 6T   
 66   �� *	FirstName+6T     5"      $ Ausweisnummer��6%   $		 150 6_   
 6<   �� *LibraryCardNum+6_   4  .System.Windows.Controls.DataGridTemplateColumn  5%      $ Aktionen��6)   $		 150 6;      CellTemplate 5&      �� 5'      .R�5(      �� $ 
Horizontal=�6)   $��Center=�6B   .+�5)   "   �� $��
Bearbeiten��6)   $��75��6>   $��0,0,5,0q�6I   ��5*   )   �� * DataContext.EditStudentCommand+ �� RelativeSource ��  �� AncestorType DataGrid��5+   )   �� �� 5,   "   $��Löschen��6)   $��75��6;   ��5-   )   �� *,*DataContext.OpenDeleteStudentWindowCommand+ ��  DataGrid��5.   )   �� 5/      50      52      54      55      56      