   M S B A M L   `   `   `  ����  Monitor    Monitor.View.RouteModify   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  线路调整��5   	   $	��300��6   $	��300��6#    � 
ResizeMode$ CanMinimize=�6/    � 	Resources 5       �� 
TargetType �� Property �� Value��%R   '��       �� 5   
   " ��6   .�5      ��  � ��$ 10q�6'   5      5	      .��5
      �  � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��线路列表��6   $��5q�6!   .��5      �� -   5        $��	routeList��5       �� DisplayMemberPath$
 Name��6K   ��6'   ��  �� Path$ Routes�6f   5      � 5   
   $��1��6   $��5q�6   	 � RowDefinitions	 5      �� 5      �� 5      .+�5      �� $��上移��6   ��6"   �� $ 	UpCommand���6E   �� 
 �� ElementName$
 	routeList��$ SelectedItem��� 5      $��1��6   $��下移��6"   ��6/   �� $ DownCommand���6T   �� $
 	routeList��$ SelectedItem�5      5      5      