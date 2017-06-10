using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.WebPages;

namespace HtmlPropertyHelperDemo
{
    public class HtmlCollectionContext<TList>
    {
        private HtmlHelper<TList> _htmlHelper;

        public HtmlCollectionContext(HtmlHelper<TList> htmlHelper, TList item)
        {
            this._htmlHelper = htmlHelper;
            this.Item = item;
        }

        public TList Item
        {
            get;
        }

        public ModelPropertyAttribute BeginProperty<TProperty>(Expression<Func<TList, TProperty>> expression)
        {
            return HtmlPropertyHelper.BeginProperty(_htmlHelper, expression);
        }
    }

    public static class HtmlCollectionHelper
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <typeparam name="TList"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="items"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        /// <example>
        /// @Html.ForCollection(x => x.Users, Model.Users,
        ///     @<div>
        ///         @using(var property = item.BeginProperty(x => x.Username))
        ///         {
        ///             <input type="text" id="@property.ID" name="@property.Name" value="@property.Value" >
        ///         }
        ///     </div>
        /// )
        /// </example>
        public static HelperResult ForCollection<TModel, TProperty, TList>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<TList> items, Func<HtmlCollectionContext<TList>, HelperResult> template)
        {
            return new HelperResult(writer =>
            {
                ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
                string expressionText = ExpressionHelper.GetExpressionText(expression);
                string name = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(expressionText);

                int i = 0;
                foreach (var item in items)
                {
                    ViewDataDictionary viewData = new ViewDataDictionary(htmlHelper.ViewDataContainer.ViewData);
                    viewData.Model = item;
                    viewData.TemplateInfo = new TemplateInfo()
                    {
                        HtmlFieldPrefix = $"{name}[{i}]"
                    };

                    ViewContext viewContext = new ViewContext(
                        htmlHelper.ViewContext.Controller.ControllerContext,
                        htmlHelper.ViewContext.View,
                        viewData,
                        htmlHelper.ViewContext.TempData,
                        htmlHelper.ViewContext.Writer
                        );

                    ViewDataContainer container = new ViewDataContainer(viewData);
                    HtmlHelper<TList> listHelper = new HtmlHelper<TList>(viewContext, container);
                    HtmlCollectionContext<TList> context = new HtmlCollectionContext<TList>(listHelper, item);

                    template(context).WriteTo(writer);

                    i++;
                }
            });
        }

        private class ViewDataContainer : IViewDataContainer
        {
            public ViewDataContainer(ViewDataDictionary viewData)
            {
                this.ViewData = viewData;
            }

            public ViewDataDictionary ViewData
            {
                get;
                set;
            }
        }
    }
}
