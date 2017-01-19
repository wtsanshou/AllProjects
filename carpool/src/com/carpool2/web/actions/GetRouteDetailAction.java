package com.carpool2.web.actions;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.struts.action.Action;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;


import com.carpool2.business.dao.UserDAO;
import com.carpool2.business.manager.CPInfoManager;
import com.carpool2.business.manager.UserManager;
import com.carpool2.business.model.CPInfo;
import com.carpool2.business.model.User;
import com.carpool2.web.forms.GetRouteDetailActionForm;

public class GetRouteDetailAction extends Action {

	/* (non-Javadoc)
	 * @see org.apache.struts.action.Action#execute(org.apache.struts.action.ActionMapping, org.apache.struts.action.ActionForm, javax.servlet.http.HttpServletRequest, javax.servlet.http.HttpServletResponse)
	 */
	@Override
	public ActionForward execute(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		boolean flag = false;
		
		GetRouteDetailActionForm rda = (GetRouteDetailActionForm)form;
		CPInfo cpi = new CPInfo();
		String routeID = rda.getRouteID();
		User publisher = new User();
		User applicant1= new User();
		User applicant2= new User();
		User applicant3= new User();
		
		CPInfoManager cim = new CPInfoManager();
		UserManager um = new UserManager();

		boolean ll = false;
		if(request.getParameter("lost") != null){
			ll = true;
		}
		
		publisher = null;
		applicant1 = null;
		applicant2 = null;
		applicant3 = null;
		try{
			cpi  = cim.findByID(routeID);
			publisher = um.findUserByID(cpi.getCpRegisterMainId().toString());
			if (!cpi.getCpRegisterId1().equals(0)){
				applicant1 = um.findUserByID(cpi.getCpRegisterId1().toString());
			}
			if (!cpi.getCpRegisterId2().equals(0)){
				applicant2 = um.findUserByID(cpi.getCpRegisterId2().toString());
			}
			if (!cpi.getCpRegisterId3().equals(0)){
				applicant3 = um.findUserByID(cpi.getCpRegisterId3().toString());
			}
	
			
			request.setAttribute("result", cpi);
			if (publisher != null){
				request.setAttribute("publisher", publisher);
				
			}
			if (applicant1 != null){
			request.setAttribute("applicant1", applicant1);
			}
			if (applicant2 != null){
			request.setAttribute("applicant2", applicant2);
			}
			if (applicant3 != null){
				request.setAttribute("applicant3", applicant3);
			}
			flag = true;
		}catch(Exception e){
			e.printStackTrace();
		}
		if(flag == true){
			if(ll){
				request.setAttribute("lost", "lost");
			}
			return mapping.findForward("success");
		}else {
			request.setAttribute("error", "≤È—ØœÍœ∏–≈œ¢ ß∞‹");
			return mapping.findForward("fail");
		}

	}
}
