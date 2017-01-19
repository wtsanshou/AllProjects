package com.carpool2.web.forms;

import org.apache.struts.action.ActionForm;

public class DeleteRouteActionForm extends ActionForm {
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	Integer RouteID;

	/**
	 * @return the routeID
	 */
	public Integer getRouteID() {
		return RouteID;
	}

	/**
	 * @param routeID the routeID to set
	 */
	public void setRouteID(Integer routeID) {
		RouteID = routeID;
	}
	
}
