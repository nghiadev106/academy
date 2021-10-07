(function($){Drupal.behaviors.ViewsAjaxView={};Drupal.behaviors.ViewsAjaxView.attach=function(){if(Drupal.settings&&Drupal.settings.views&&Drupal.settings.views.ajaxViews){$.each(Drupal.settings.views.ajaxViews,function(i,settings){Drupal.views.instances[i]=new Drupal.views.ajaxView(settings);});}};Drupal.views={};Drupal.views.instances={};Drupal.views.ajaxView=function(settings){var selector='.view-dom-id-'+settings.view_dom_id;this.$view=$(selector);var ajax_path=Drupal.settings.views.ajax_path;if(ajax_path.constructor.toString().indexOf("Array")!=-1){ajax_path=ajax_path[0];}
var queryString=window.location.search||'';if(queryString!==''){var queryString=queryString.slice(1).replace(/q=[^&]+&?|&?render=[^&]+/,'');if(queryString!==''){queryString=((/\?/.test(ajax_path))?'&':'?')+queryString;}}
this.element_settings={url:ajax_path+queryString,submit:settings,setClick:true,event:'click',selector:selector,progress:{type:'throbber'}};this.settings=settings;this.$exposed_form=$('#views-exposed-form-'+settings.view_name.replace(/_/g,'-')+'-'+settings.view_display_id.replace(/_/g,'-'));this.$exposed_form.once(jQuery.proxy(this.attachExposedFormAjax,this));this.links=[];this.$view.once(jQuery.proxy(this.attachPagerAjax,this));var self_settings=this.element_settings;self_settings.event='RefreshView';this.refreshViewAjax=new Drupal.ajax(this.selector,this.$view,self_settings);};Drupal.views.ajaxView.prototype.attachExposedFormAjax=function(){var button=$('input[type=submit], button[type=submit], input[type=image]',this.$exposed_form);button=button[0];$(button).click(function(){if(Drupal.autocompleteSubmit){Drupal.autocompleteSubmit();}});this.exposedFormAjax=new Drupal.ajax($(button).attr('id'),button,this.element_settings);};Drupal.views.ajaxView.prototype.attachPagerAjax=function(){this.$view.find('ul.pager > li > a, th.views-field a, .attachment .views-summary a').each(jQuery.proxy(this.attachPagerLinkAjax,this));};Drupal.views.ajaxView.prototype.attachPagerLinkAjax=function(id,link){var $link=$(link);if($link.closest('.view')[0]!==this.$view[0]){return;}
var viewData={};var href=$link.attr('href');if(typeof(viewData.page)==='undefined'){viewData.page=0;}
$.extend(viewData,this.settings,Drupal.Views.parseQueryString(href),Drupal.Views.parseViewArgs(href,this.settings.view_base_path));$.extend(viewData,Drupal.Views.parseViewArgs(href,this.settings.view_base_path));this.element_settings.submit=viewData;this.pagerAjax=new Drupal.ajax(false,$link,this.element_settings);this.links.push(this.pagerAjax);};Drupal.ajax.prototype.commands.viewsScrollTop=function(ajax,response,status){var offset=$(response.selector).offset();var scrollTarget=response.selector;while($(scrollTarget).scrollTop()==0&&$(scrollTarget).parent()){scrollTarget=$(scrollTarget).parent();}
if(offset.top-10<$(scrollTarget).scrollTop()){$(scrollTarget).animate({scrollTop:(offset.top-10)},500);}};})(jQuery);