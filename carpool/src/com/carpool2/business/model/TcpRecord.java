package com.carpool2.business.model;

import java.util.Date;


/**
 * TcpRecord entity.
 * 
 * @author MyEclipse Persistence Tools
 */

public class TcpRecord implements java.io.Serializable {

	// Fields

	private Integer cpRecordId;
	private Integer tuser;
	private Integer tcpInf;
	private Byte cpRecordUserType;
	private Integer cpRecordTime;
	private Date cpRecordStatus;
	private Integer cpRecordComment;
	private Integer del;
	private String test2;
	private String test3;

	// Constructors

	/** default constructor */
	public TcpRecord() {
	}

	/** full constructor */
	public TcpRecord(Integer tuser, Integer tcpInf, Byte cpRecordUserType,
			Integer cpRecordTime, Date cpRecordStatus, Integer cpRecordComment,
			Integer del, String test2, String test3) {
		this.tuser = tuser;
		this.tcpInf = tcpInf;
		this.cpRecordUserType = cpRecordUserType;
		this.cpRecordTime = cpRecordTime;
		this.cpRecordStatus = cpRecordStatus;
		this.cpRecordComment = cpRecordComment;
		this.del = del;
		this.test2 = test2;
		this.test3 = test3;
	}

	// Property accessors

	public Integer getCpRecordId() {
		return this.cpRecordId;
	}

	public void setCpRecordId(Integer cpRecordId) {
		this.cpRecordId = cpRecordId;
	}

	public Integer getTuser() {
		return this.tuser;
	}

	public void setTuser(Integer tuser) {
		this.tuser = tuser;
	}

	public Integer getTcpInf() {
		return this.tcpInf;
	}

	public void setTcpInf(Integer tcpInf) {
		this.tcpInf = tcpInf;
	}

	public Byte getCpRecordUserType() {
		return this.cpRecordUserType;
	}

	public void setCpRecordUserType(Byte cpRecordUserType) {
		this.cpRecordUserType = cpRecordUserType;
	}

	public Integer getCpRecordTime() {
		return this.cpRecordTime;
	}

	public void setCpRecordTime(Integer cpRecordTime) {
		this.cpRecordTime = cpRecordTime;
	}

	public Date getCpRecordStatus() {
		return this.cpRecordStatus;
	}

	public void setCpRecordStatus(Date cpRecordStatus) {
		this.cpRecordStatus = cpRecordStatus;
	}

	public Integer getCpRecordComment() {
		return this.cpRecordComment;
	}

	public void setCpRecordComment(Integer cpRecordComment) {
		this.cpRecordComment = cpRecordComment;
	}

	public Integer getDel() {
		return this.del;
	}

	public void setDel(Integer del) {
		this.del = del;
	}

	public String getTest2() {
		return this.test2;
	}

	public void setTest2(String test2) {
		this.test2 = test2;
	}

	public String getTest3() {
		return this.test3;
	}

	public void setTest3(String test3) {
		this.test3 = test3;
	}

}