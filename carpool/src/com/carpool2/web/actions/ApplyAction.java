package com.carpool2.web.actions;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.struts.action.Action;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import com.carpool2.business.manager.CPInfoManager;
import com.carpool2.business.manager.TcpRecordManager;
import com.carpool2.business.manager.UserManager;
import com.carpool2.business.model.CPInfo;
import com.carpool2.business.model.TcpRecord;


public class ApplyAction extends Action {


	/* (non-Javadoc)
	 * @see org.apache.struts.action.Action#execute(org.apache.struts.action.ActionMapping, org.apache.struts.action.ActionForm, javax.servlet.http.HttpServletRequest, javax.servlet.http.HttpServletResponse)
	 */
	@Override
	public ActionForward execute(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		boolean flag = false;
		String routeID = request.getParameter("routeID").toString();
		String userID = request.getParameter("userID").toString();
		TcpRecordManager trm = new TcpRecordManager();
		TcpRecord tr = new TcpRecord();
		UserManager um = new UserManager();
		CPInfoManager cim = new CPInfoManager();
		CPInfo cpi = new CPInfo();
		
		
		try{
			um.applyRoute(userID, routeID);
			cpi = cim.findByID(routeID);

			
			if(cpi.getCpInfPeopNumb().equals(cpi.getCpInfAvilableNum())) {
	
				trm.dispatchInfo(cpi); //发送撮合成功消息
			}
			flag = true;
		}catch(Exception e){
			e.printStackTrace();
		}
		if (flag == true){
			tr = trm.getLatestByUserID(Integer.parseInt(userID));
			if(tr != null){
				request.getSession().setAttribute("news", "您有新的消息！");
			}
			request.getSession().setAttribute("message", tr);
			request.getSession().setAttribute("applied", cpi);
			return mapping.findForward("applied");
		}else
		{
			request.setAttribute("error", "报名失败");
			return mapping.findForward("fail");
		}
	}
}
