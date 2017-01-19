package com.carpool2.web.forms;

import java.util.Date;

import org.apache.struts.action.ActionForm;

public class PublicActionForm extends ActionForm {
	private String userName;
	private int userType;
	private int limit;//人数限制
	private String sex;//性别限制
	private String start;
	private String end;
	private String scord;//起点坐标
	private String ecord;//终点坐标
	private String depatureTime;
	private String date;
	private String hour;
	private String minute;
	private String status;
	private String remark;//拼车备注
	private String frequency;
	/**
	 * @return the userName
	 */
	public String getUserName() {
		return userName;
	}
	/**
	 * @param userName the userName to set
	 */
	public void setUserName(String userName) {
		this.userName = userName;
	}
	/**
	 * @return the userType
	 */
	public int getUserType() {
		return userType;
	}
	/**
	 * @param userType the userType to set
	 */
	public void setUserType(int userType) {
		this.userType = userType;
	}
	/**
	 * @return the limit
	 */
	public int getLimit() {
		return limit;
	}
	/**
	 * @param limit the limit to set
	 */
	public void setLimit(int limit) {
		this.limit = limit;
	}
	/**
	 * @return the sex
	 */
	public String getSex() {
		return sex;
	}
	/**
	 * @param sex the sex to set
	 */
	public void setSex(String sex) {
		this.sex = sex;
	}
	/**
	 * @return the start
	 */
	public String getStart() {
		return start;
	}
	/**
	 * @param start the start to set
	 */
	public void setStart(String start) {
		this.start = start;
	}
	/**
	 * @return the end
	 */
	public String getEnd() {
		return end;
	}
	/**
	 * @param end the end to set
	 */
	public void setEnd(String end) {
		this.end = end;
	}
	/**
	 * @return the scord
	 */
	public String getScord() {
		return scord;
	}
	/**
	 * @param scord the scord to set
	 */
	public void setScord(String scord) {
		this.scord = scord;
	}
	/**
	 * @return the ecord
	 */
	public String getEcord() {
		return ecord;
	}
	/**
	 * @param ecord the ecord to set
	 */
	public void setEcord(String ecord) {
		this.ecord = ecord;
	}
	/**
	 * @return the depatureTime
	 */
	public String getDepatureTime() {
		return depatureTime;
	}
	/**
	 * @param depatureTime the depatureTime to set
	 */
	public void setDepatureTime(String depatureTime) {
		this.depatureTime = depatureTime;
	}
	/**
	 * @return the status
	 */
	public String getStatus() {
		return status;
	}
	/**
	 * @param status the status to set
	 */
	public void setStatus(String status) {
		this.status = status;
	}
	/**
	 * @return the remark
	 */
	public String getRemark() {
		return remark;
	}
	/**
	 * @param remark the remark to set
	 */
	public void setRemark(String remark) {
		this.remark = remark;
	}
	/**
	 * @return the frequency
	 */
	public String getFrequency() {
		return frequency;
	}
	/**
	 * @param frequency the frequency to set
	 */
	public void setFrequency(String frequency) {
		this.frequency = frequency;
	}
	/**
	 * @return the date
	 */
	public String getDate() {
		return date;
	}
	/**
	 * @param date the date to set
	 */
	public void setDate(String date) {
		this.date = date;
	}
	/**
	 * @return the hour
	 */
	public String getHour() {
		return hour;
	}
	/**
	 * @param hour the hour to set
	 */
	public void setHour(String hour) {
		this.hour = hour;
	}
	/**
	 * @return the minute
	 */
	public String getMinute() {
		return minute;
	}
	/**
	 * @param minute the minute to set
	 */
	public void setMinute(String minute) {
		this.minute = minute;
	}

}