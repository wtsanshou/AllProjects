/**
 * 
 */
package com.carpool2.web.forms;

import org.apache.struts.action.ActionForm;

/**
 * @author Administrator
 *
 */
@SuppressWarnings("serial")
public class LoginActionForm extends ActionForm {
	
	private String username;
	
	private String password;

	public String getUsername() {
		return username;
	}

	public void setUsername(String username) {
		this.username = username;
	}

	public String getPassword() {
		return password;
	}

	public void setPassword(String password) {
		this.password = password;
	}
		
}
