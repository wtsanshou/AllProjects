package com.carpool2.web.forms;

import org.apache.struts.action.ActionForm;

public class GetRouteDetailActionForm extends ActionForm{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	String routeID;

	/**
	 * @return the routeID
	 */
	public String getRouteID() {
		return routeID;
	}

	/**
	 * @param routeID the routeID to set
	 */
	public void setRouteID(String routeID) {
		this.routeID = routeID;
	}
}
