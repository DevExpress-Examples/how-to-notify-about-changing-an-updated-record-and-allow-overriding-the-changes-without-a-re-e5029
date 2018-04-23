Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports DevExpress.Web.Mvc

Namespace DevExpressMvcApplication1.Controllers
	Public Class HomeController
		Inherits Controller
		Public Function Index() As ActionResult
			ViewBag.Message = "Welcome to DevExpress Extensions for ASP.NET MVC!"

			Return View(NorthwindDataProvider.GetProducts())
		End Function

		Public Function InlineEditingPartial() As ActionResult
			Return PartialView("GridView", NorthwindDataProvider.GetEditableProducts())
		End Function

		<HttpPost> _
		Public Function InlineEditingUpdatePartial(<ModelBinder(GetType(DevExpressEditorsBinder))> ByVal product As EditableProduct, Optional ByVal forceUpdate As Boolean = False) As ActionResult
			If ModelState.IsValid Then
				Try
					Dim dbCheckResult As Boolean = False 'checking is failed, for demo only

					Dim canUpdate As Boolean = dbCheckResult OrElse forceUpdate
					ViewData("canUpdate") = canUpdate
					If canUpdate Then
						'Update is not allowed in online demos
						'NorthwindDataProvider.UpdateProduct(product);
					Else
						ViewData("editedRowKey") = product.ProductID
					End If
				Catch e As Exception
					ViewData("EditError") = e.Message
				End Try
			Else
				ViewData("EditError") = "Please, correct all errors."
			End If

			Return PartialView("GridView", NorthwindDataProvider.GetEditableProducts())
		End Function

	End Class
End Namespace
