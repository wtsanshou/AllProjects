package com.carpool2.business.model;
// default package

import java.util.Date;
import java.util.HashSet;
import java.util.Set;


/**
 * CPInfo entity. @author MyEclipse Persistence Tools
 */

public class CPInfo  implements java.io.Serializable {


    // Fields    

     /**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private Integer cpInfId;
	
     private User tuser;
     private Byte cpInfUserType;
     private Integer cpInfPeopNumb;
     private Integer cpInfAvilableNum;
     private String cpInfSex;
     private Date cpInfPublishTime;
     private String cpInfStart;
     private String cpInfScoordinate;
     private String cpInfEnd;
     private String cpInfEcoordinate;
     private String cpInfDepartTime;
     private String cpInfStatus;
     private String cpInfRemark;
     private Integer del;
     private String cpInfFrequency;
     private String test3;
     private Date cpRegisterTime;
     private Integer cpRegisterMainId;
     private Integer cpRegisterId1;
     private Integer cpRegisterId2;
     private Integer cpRegisterId3;
     private Set tcpRecords = new HashSet(0);


    // Constructors

    /** default constructor */
    public CPInfo() {
    }

    
    /** full constructor */
    public CPInfo(User tuser, Byte cpInfUserType, Integer cpInfPeopNumb, Integer cpInfAvilableNum, String cpInfSex, Date cpInfPublishTime, String cpInfStart, String cpInfScoordinate, String cpInfEnd, String cpInfEcoordinate, String cpInfDepartTime, String cpInfStatus, String cpInfRemark, Integer del, String cpInfFrequency, String test3, Date cpRegisterTime, Integer cpRegisterMainId, Integer cpRegisterId1, Integer cpRegisterId2, Integer cpRegisterId3, Set tcpRecords ) {
        this.tuser = tuser;
        this.cpInfUserType = cpInfUserType;
        this.cpInfPeopNumb = cpInfPeopNumb;
        this.cpInfAvilableNum = cpInfAvilableNum;
        this.cpInfSex = cpInfSex;
        this.cpInfPublishTime = cpInfPublishTime;
        this.cpInfStart = cpInfStart;
        this.cpInfScoordinate = cpInfScoordinate;
        this.cpInfEnd = cpInfEnd;
        this.cpInfEcoordinate = cpInfEcoordinate;
        this.cpInfDepartTime = cpInfDepartTime;
        this.cpInfStatus = cpInfStatus;
        this.cpInfRemark = cpInfRemark;
        this.del = del;
        this.cpInfFrequency = cpInfFrequency;
        this.test3 = test3;
        this.cpRegisterTime = cpRegisterTime;
        this.cpRegisterMainId = cpRegisterMainId;
        this.cpRegisterId1 = cpRegisterId1;
        this.cpRegisterId2 = cpRegisterId2;
        this.cpRegisterId3 = cpRegisterId3;
        this.tcpRecords = tcpRecords;
       
    }

   
    // Property accessors

    public Integer getCpInfId() {
        return this.cpInfId;
    }
    
    public void setCpInfId(Integer cpInfId) {
        this.cpInfId = cpInfId;
    }

    public User getTuser() {
        return this.tuser;
    }
    
    public void setTuser(User tuser) {
        this.tuser = tuser;
    }

    public Byte getCpInfUserType() {
        return this.cpInfUserType;
    }
    
    public void setCpInfUserType(Byte cpInfUserType) {
        this.cpInfUserType = cpInfUserType;
    }

    public Integer getCpInfPeopNumb() {
        return this.cpInfPeopNumb;
    }
    
    public void setCpInfPeopNumb(Integer cpInfPeopNumb) {
        this.cpInfPeopNumb = cpInfPeopNumb;
    }

    public Integer getCpInfAvilableNum() {
        return this.cpInfAvilableNum;
    }
    
    public void setCpInfAvilableNum(Integer cpInfAvilableNum) {
        this.cpInfAvilableNum = cpInfAvilableNum;
    }

    public String getCpInfSex() {
        return this.cpInfSex;
    }
    
    public void setCpInfSex(String cpInfSex) {
        this.cpInfSex = cpInfSex;
    }

    public Date getCpInfPublishTime() {
        return this.cpInfPublishTime;
    }
    
    public void setCpInfPublishTime(Date cpInfPublishTime) {
        this.cpInfPublishTime = cpInfPublishTime;
    }

    public String getCpInfStart() {
        return this.cpInfStart;
    }
    
    public void setCpInfStart(String cpInfStart) {
        this.cpInfStart = cpInfStart;
    }

    public String getCpInfScoordinate() {
        return this.cpInfScoordinate;
    }
    
    public void setCpInfScoordinate(String cpInfScoordinate) {
        this.cpInfScoordinate = cpInfScoordinate;
    }

    public String getCpInfEnd() {
        return this.cpInfEnd;
    }
    
    public void setCpInfEnd(String cpInfEnd) {
        this.cpInfEnd = cpInfEnd;
    }

    public String getCpInfEcoordinate() {
        return this.cpInfEcoordinate;
    }
    
    public void setCpInfEcoordinate(String cpInfEcoordinate) {
        this.cpInfEcoordinate = cpInfEcoordinate;
    }

    public String getCpInfDepartTime() {
        return this.cpInfDepartTime;
    }
    
    public void setCpInfDepartTime(String cpInfDepartTime) {
        this.cpInfDepartTime = cpInfDepartTime;
    }

    public String getCpInfStatus() {
        return this.cpInfStatus;
    }
    
    public void setCpInfStatus(String cpInfStatus) {
        this.cpInfStatus = cpInfStatus;
    }

    public String getCpInfRemark() {
        return this.cpInfRemark;
    }
    
    public void setCpInfRemark(String cpInfRemark) {
        this.cpInfRemark = cpInfRemark;
    }

    public Integer getDel() {
        return this.del;
    }
    
    public void setDel(Integer del) {
        this.del = del;
    }

    public String getCpInfFrequency() {
        return this.cpInfFrequency;
    }
    
    public void setCpInfFrequency(String cpInfFrequency) {
        this.cpInfFrequency = cpInfFrequency;
    }

    public String getTest3() {
        return this.test3;
    }
    
    public void setTest3(String test3) {
        this.test3 = test3;
    }

    public Date getCpRegisterTime() {
        return this.cpRegisterTime;
    }
    
    public void setCpRegisterTime(Date cpRegisterTime) {
        this.cpRegisterTime = cpRegisterTime;
    }

    public Integer getCpRegisterMainId() {
        return this.cpRegisterMainId;
    }
    
    public void setCpRegisterMainId(Integer cpRegisterMainId) {
        this.cpRegisterMainId = cpRegisterMainId;
    }

    public Integer getCpRegisterId1() {
        return this.cpRegisterId1;
    }
    
    public void setCpRegisterId1(Integer cpRegisterId1) {
        this.cpRegisterId1 = cpRegisterId1;
    }

    public Integer getCpRegisterId2() {
        return this.cpRegisterId2;
    }
    
    public void setCpRegisterId2(Integer cpRegisterId2) {
        this.cpRegisterId2 = cpRegisterId2;
    }

    public Integer getCpRegisterId3() {
        return this.cpRegisterId3;
    }
    
    public void setCpRegisterId3(Integer cpRegisterId3) {
        this.cpRegisterId3 = cpRegisterId3;
    }

    public Set getTcpRecords() {
        return this.tcpRecords;
    }
    
    public void setTcpRecords(Set tcpRecords) {
        this.tcpRecords = tcpRecords;
    }
    public boolean equals(Object obj){
    	if(this == obj)
    	{
    		return true;
    	}
    	if(obj != null && obj.getClass()== CPInfo.class){
    		CPInfo cpi = (CPInfo)obj;
    		return this.getCpInfId().equals(cpi.getCpInfId());
    	}
    	return false;
    }


	@Override
	public int hashCode() {
		return test3.hashCode();
	}
    
    
}