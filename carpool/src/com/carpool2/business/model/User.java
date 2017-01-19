package com.carpool2.business.model;
// default package

import java.util.Date;
import java.util.HashSet;
import java.util.Set;


/**
 * User entity. @author MyEclipse Persistence Tools
 */

public class User  implements java.io.Serializable {


    // Fields    

     /**
	 * 
	 */
	private static final long serialVersionUID = 7092026196255778592L;
	private Integer userId;
     private String userName;
     private String userNickname;
     private String userPwd;
     private String userSex;
     private Date userRegTime;
     private Integer userLoginTimes;
     private Integer userGoodTimes;
     private Integer userBadTimes;
     private Integer userLevel;
     private String userEmail;
     private Integer userSuccessTimes;
     private String userContact;
     private String userProfile;
     private String userIndr;
     private Integer del;
     private String test;
     private Set tcpInfs = new HashSet(0);
     private Set tcpRecords = new HashSet(0);


    // Constructors

    /** default constructor */
    public User() {
    }

    
    /** full constructor */
    public User(String userName, String userNickname, String userPwd, String userSex, Date userRegTime, Integer userLoginTimes, Integer userGoodTimes, Integer userBadTimes, Integer userLevel, String userEmail, Integer userSuccessTimes, String userContact, String userProfile, String userIndr, Integer del, String test, Set tcpInfs, Set tcpRecords) {
        this.userName = userName;
        this.userNickname = userNickname;
        this.userPwd = userPwd;
        this.userSex = userSex;
        this.userRegTime = userRegTime;
        this.userLoginTimes = userLoginTimes;
        this.userGoodTimes = userGoodTimes;
        this.userBadTimes = userBadTimes;
        this.userLevel = userLevel;
        this.userEmail = userEmail;
        this.userSuccessTimes = userSuccessTimes;
        this.userContact = userContact;
        this.userProfile = userProfile;
        this.userIndr = userIndr;
        this.del = del;
        this.test = test;
        this.tcpInfs = tcpInfs;
        this.tcpRecords = tcpRecords;
    }

   
    // Property accessors

    public Integer getUserId() {
        return this.userId;
    }
    
    public void setUserId(Integer userId) {
        this.userId = userId;
    }

    public String getUserName() {
        return this.userName;
    }
    
    public void setUserName(String userName) {
        this.userName = userName;
    }

    public String getUserNickname() {
        return this.userNickname;
    }
    
    public void setUserNickname(String userNickname) {
        this.userNickname = userNickname;
    }

    public String getUserPwd() {
        return this.userPwd;
    }
    
    public void setUserPwd(String userPwd) {
        this.userPwd = userPwd;
    }

    public String getUserSex() {
        return this.userSex;
    }
    
    public void setUserSex(String userSex) {
        this.userSex = userSex;
    }

    public Date getUserRegTime() {
        return this.userRegTime;
    }
    
    public void setUserRegTime(Date userRegTime) {
        this.userRegTime = userRegTime;
    }

    public Integer getUserLoginTimes() {
        return this.userLoginTimes;
    }
    
    public void setUserLoginTimes(Integer userLoginTimes) {
        this.userLoginTimes = userLoginTimes;
    }

    public Integer getUserGoodTimes() {
        return this.userGoodTimes;
    }
    
    public void setUserGoodTimes(Integer userGoodTimes) {
        this.userGoodTimes = userGoodTimes;
    }

    public Integer getUserBadTimes() {
        return this.userBadTimes;
    }
    
    public void setUserBadTimes(Integer userBadTimes) {
        this.userBadTimes = userBadTimes;
    }

    public Integer getUserLevel() {
        return this.userLevel;
    }
    
    public void setUserLevel(Integer userLevel) {
        this.userLevel = userLevel;
    }

    public String getUserEmail() {
        return this.userEmail;
    }
    
    public void setUserEmail(String userEmail) {
        this.userEmail = userEmail;
    }

    public Integer getUserSuccessTimes() {
        return this.userSuccessTimes;
    }
    
    public void setUserSuccessTimes(Integer userSuccessTimes) {
        this.userSuccessTimes = userSuccessTimes;
    }

    public String getUserContact() {
        return this.userContact;
    }
    
    public void setUserContact(String userContact) {
        this.userContact = userContact;
    }

    public String getUserProfile() {
        return this.userProfile;
    }
    
    public void setUserProfile(String userProfile) {
        this.userProfile = userProfile;
    }

    public String getUserIndr() {
        return this.userIndr;
    }
    
    public void setUserIndr(String userIndr) {
        this.userIndr = userIndr;
    }

    public Integer getDel() {
        return this.del;
    }
    
    public void setDel(Integer del) {
        this.del = del;
    }

    public String getTest() {
        return this.test;
    }
    
    public void setTest(String test) {
        this.test = test;
    }

    public Set getTcpInfs() {
        return this.tcpInfs;
    }
    
    public void setTcpInfs(Set tcpInfs) {
        this.tcpInfs = tcpInfs;
    }

    public Set getTcpRecords() {
        return this.tcpRecords;
    }
    
    public void setTcpRecords(Set tcpRecords) {
        this.tcpRecords = tcpRecords;
    }
   








}