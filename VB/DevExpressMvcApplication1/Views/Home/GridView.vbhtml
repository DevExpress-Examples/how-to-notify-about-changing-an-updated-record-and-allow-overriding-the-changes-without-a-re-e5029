@ModelType System.Collections.IEnumerable
@Code	 
	Dim grid = Html.DevExpress().GridView(Sub(settings)
		

													 settings.Name = "gvEditing"
													 settings.KeyFieldName = "ProductID"
													 settings.CallbackRouteValues = New With {Key .Controller = "Home", Key .Action = "InlineEditingPartial"}
													 settings.SettingsEditing.UpdateRowRouteValues = New With {Key .Controller = "Home", Key .Action = "InlineEditingUpdatePartial"}
													 settings.SettingsEditing.Mode = GridViewEditingMode.EditFormAndDisplayRow

													 settings.CommandColumn.Visible = True
													 settings.CommandColumn.EditButton.Visible = True

													 settings.ClientSideEvents.EndCallback = "OnEndCallback"
													 settings.ClientSideEvents.BeginCallback = "OnBeginCallback"

													 settings.Columns.Add("ProductName")
													 settings.Columns.Add(Sub(column)
																				  column.FieldName = "CategoryID"
																				  column.Caption = "Category"
																				  column.ColumnType = MVCxGridViewColumnType.ComboBox
																				  Dim comboBoxProperties = TryCast(column.PropertiesEdit, ComboBoxProperties)
																				  comboBoxProperties.DataSource = NorthwindDataProvider.GetCategories()
																				  comboBoxProperties.TextField = "CategoryName"
																				  comboBoxProperties.ValueField = "CategoryID"
																				  comboBoxProperties.ValueType = GetType(Integer)
																		  End Sub)
											   
													 settings.Columns.Add("QuantityPerUnit")
													 settings.Columns.Add(Sub(column)


																				  column.FieldName = "UnitPrice"

																				  column.ColumnType = MVCxGridViewColumnType.SpinEdit
																				  Dim spinEditProperties = TryCast(column.PropertiesEdit, SpinEditProperties)
																				  spinEditProperties.DisplayFormatString = "c"
																				  spinEditProperties.DisplayFormatInEditMode = True
																				  spinEditProperties.MinValue = 0
																				  spinEditProperties.MaxValue = 1000000
																				  spinEditProperties.SpinButtons.ShowLargeIncrementButtons = True
																		  End Sub)
								 
													 settings.Columns.Add(Sub(column)
																				  column.FieldName = "UnitsInStock"

																				  column.ColumnType = MVCxGridViewColumnType.SpinEdit
																				  Dim spinEditProperties = TryCast(column.PropertiesEdit, SpinEditProperties)
																				  spinEditProperties.NumberType = SpinEditNumberType.Integer
																				  spinEditProperties.MinValue = 0
																				  spinEditProperties.MaxValue = 10000
																		  End Sub)
											   
													 settings.Columns.Add("Discontinued", MVCxGridViewColumnType.CheckBox)

													 settings.RowValidating = Sub(s, e)
																					  Dim canUpdate As Boolean = CBool(If(ViewData("canUpdate"), False))
																					  If (Not canUpdate) Then
																						  Dim g As MVCxGridView = TryCast(s, MVCxGridView)
																						  g.JSProperties("cpRecordIsModified") = True
																						  e.RowError = "Recond has been updated by another user. Do you wish to override user changed?"
																					  End If
																			  End Sub
				
											 End Sub)


	If ViewData("EditError") IsNot Nothing Then
		grid.SetEditErrorText(CStr(ViewData("EditError")))
	End If
End Code
@grid.Bind(Model).GetHtml()
