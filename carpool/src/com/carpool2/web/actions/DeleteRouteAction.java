package com.carpool2.web.actions;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.struts.action.Action;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import com.carpool2.business.manager.CPInfoManager;
import com.carpool2.web.forms.DeleteRouteActionForm;

public class DeleteRouteAction extends Action {

	/* (non-Javadoc)
	 * @see org.apache.struts.action.Action#execute(org.apache.struts.action.ActionMapping, org.apache.struts.action.ActionForm, javax.servlet.http.HttpServletRequest, javax.servlet.http.HttpServletResponse)
	 */
	@Override
	public ActionForward execute(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		DeleteRouteActionForm daf = (DeleteRouteActionForm)form;
		CPInfoManager cim = new CPInfoManager();
		try{
			cim.deleteInfoByID(daf.getRouteID());
			request.getSession().removeAttribute("published");
			return mapping.findForward("success");
		}catch(Exception e){
			e.printStackTrace();
			request.setAttribute("error", "取消线路失败");
			return mapping.findForward("fail");
		}
	}
}
