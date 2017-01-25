<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="xml" version="1.0" encoding = "UTF-8" indent="yes"/>
    <xsl:template match="/">
        <xsl:variable name="adder">http://staroetv.su</xsl:variable> 
        <forumInfo>
        <themes>
            <xsl:for-each select="//a[@class='forum']">
                <themeLink>
                    <xsl:attribute name="themeSubject"><xsl:value-of select="text()"></xsl:value-of></xsl:attribute>
                    <xsl:attribute name="themeUrl"><xsl:value-of select="concat( $adder,@href)"></xsl:value-of></xsl:attribute>
                </themeLink>
            </xsl:for-each>
        </themes>
        
        <threads>
        <xsl:for-each select="//a[@class='threadLink']">
            <threadLink>
                <xsl:attribute name="threadSubject"><xsl:value-of select="text()"></xsl:value-of></xsl:attribute>
                <xsl:attribute name="threadUrl"><xsl:value-of select="concat( $adder,@href)"></xsl:value-of></xsl:attribute>
            </threadLink>
        </xsl:for-each>
        </threads>
        
        <posts>
        <xsl:for-each select="//table[@class = 'postTable']">
            <post>
                <xsl:attribute name="postAuthor"><xsl:value-of select=".//a[@class = 'postUser']/text()"/></xsl:attribute>
                <xsl:attribute name="postContent"><xsl:value-of select=".//span[@class = 'ucoz-forum-post']"></xsl:value-of></xsl:attribute>
            </post>
        </xsl:for-each>
        </posts>
            <encoding>
                     <xsl:attribute name="currentEncoding"><xsl:value-of select=".//meta/@content"/></xsl:attribute>
            </encoding>
        </forumInfo>
    </xsl:template>
</xsl:stylesheet>
