   M S B A M L   `   `   `  ����=  9bd, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:bdbd  5   	       bd.AddLocationWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   "localclr-namespace:bd     5   	     � Title$(  "Месторасположение��5   	   $	��400��6#   $	��500��60    � WindowStartupLocation$ CenterScreen=�6<    +� FocusedElement 5	   	   ��  �� ElementName$ LocationTxb��.��5
      � .+�5   
   ��  �� Orientation$ Vertical=�6   $��Center=�5      $	��Top=�5      .+�5      �� $*��$Месторасположение: ��6   $
��12pt(�5      $��5q�5       �� TextAlignment$ Center=�5      �� 5      -   5        $��LocList��5      $
��12pt(�6&   $	��300��66   $	��240��6B   $
��Auto=�6O   $��Hidden=�6    �� View 5      �� 5      ! �� ColumnHeaderContainerStyle 5      �� 5       �� 
TargetType" ��6$   .�5   "   �� 	 �� Property	 � y�
 �� Value$
 	Collapsed=�6?   5      .P�5      ��  �� DisplayMemberBinding 6)   ��  �� Path$ Location� �� Width 6X   �� $ LocList��$ ActualWidth�5       � ContextMenu 5      �� 5      .@�5      m� -   5        $��ContextMenuEditBtn��5   5   $��Изменить��6#   6O   m� 5       -   5        $��ContextMenuDelBtn��5    4   $��Удалить��6#   6M   5!      5#      5$      �� 5%   
   $��Center=�6   $��Bottom=�5&      $ Vertical=�5'      $��15q�5(      .+�5)      �� $ 
Horizontal=�6   .+�5*      �� $*��$Месторасположение: ��6   $
��12pt(�5+      $��2q�5,      �� 5-      -   5        $��LocationTxb��52      $	��150��5-      $
��12pt(�5.      $��2q�5/      
���   �$��1q�51      52      53      �� 54      -   5        $��	AddLocBtn��58      $�� Добавить ��54      $
��12pt(�55      $	��120��56      $��10q�57      59      5:      5;      5<      