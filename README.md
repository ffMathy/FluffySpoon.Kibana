# FluffySpoon.Kibana
Provides helper tools for Kibana such as URL parsing.

## Input Kibana URL
> http://localhost/app/kibana#/dashboard/722b74f0-b882-11e8-a6d9-e546fe2bba5f?_g=(refreshInterval:(pause:!f,value:900000),time:(from:'2019-03-15T07:23:58.721Z',mode:absolute,to:'2019-03-18T10:43:28.880Z'))&_a=(description:'Analyze%20mock%20eCommerce%20orders%20and%20revenue',filters:!(('$state':(store:appState),meta:(alias:!n,controlledBy:'1536977437774',disabled:!f,index:ff959d40-b880-11e8-a6d9-e546fe2bba5f,key:manufacturer.keyword,negate:!f,params:!(Angeldale,'Champion%20Arts'),type:phrases,value:'Angeldale,%20Champion%20Arts'),query:(bool:(minimum_should_match:1,should:!((match_phrase:(manufacturer.keyword:Angeldale)),(match_phrase:(manufacturer.keyword:'Champion%20Arts')))))),('$state':(store:appState),meta:(alias:!n,disabled:!f,index:ff959d40-b880-11e8-a6d9-e546fe2bba5f,key:customer_gender,negate:!f,params:(query:MALE,type:phrase),type:phrase,value:MALE),query:(match:(customer_gender:(query:MALE,type:phrase)))),('$state':(store:appState),meta:(alias:!n,disabled:!f,index:ff959d40-b880-11e8-a6d9-e546fe2bba5f,key:geoip.country_iso_code,negate:!f,params:(query:FR,type:phrase),type:phrase,value:FR),query:(match:(geoip.country_iso_code:(query:FR,type:phrase)))),('$state':(store:appState),meta:(alias:!n,controlledBy:'1536977465554',disabled:!f,index:ff959d40-b880-11e8-a6d9-e546fe2bba5f,key:category.keyword,negate:!f,params:(query:'Men!'s%20Accessories',type:phrase),type:phrase,value:'Men!'s%20Accessories'),query:(match:(category.keyword:(query:'Men!'s%20Accessories',type:phrase)))),('$state':(store:appState),meta:(alias:!n,controlledBy:'1536977596163',disabled:!f,index:ff959d40-b880-11e8-a6d9-e546fe2bba5f,key:total_quantity,negate:!f,params:(gte:2,lte:4),type:range,value:'2%20to%204'),range:(total_quantity:(gte:2,lte:4)))),fullScreenMode:!f,options:(darkTheme:!f,hidePanelTitles:!f,useMargins:!t),panels:!((embeddableConfig:(vis:(colors:('Men!'s%20Accessories':%2382B5D8,'Men!'s%20Clothing':%23F9BA8F,'Men!'s%20Shoes':%23F29191,'Women!'s%20Accessories':%23F4D598,'Women!'s%20Clothing':%2370DBED,'Women!'s%20Shoes':%23B7DBAB))),gridData:(h:10,i:'1',w:36,x:12,y:18),id:'37cc8650-b882-11e8-a6d9-e546fe2bba5f',panelIndex:'1',type:visualization,version:'7.0.0-alpha1'),(embeddableConfig:(vis:(colors:(FEMALE:%236ED0E0,MALE:%23447EBC),legendOpen:!f)),gridData:(h:11,i:'2',w:12,x:12,y:7),id:ed8436b0-b88b-11e8-a6d9-e546fe2bba5f,panelIndex:'2',type:visualization,version:'7.0.0-alpha1'),(embeddableConfig:(),gridData:(h:7,i:'3',w:18,x:0,y:0),id:'09ffee60-b88c-11e8-a6d9-e546fe2bba5f',panelIndex:'3',type:visualization,version:'7.0.0-alpha1'),(embeddableConfig:(),gridData:(h:7,i:'4',w:30,x:18,y:0),id:'1c389590-b88d-11e8-a6d9-e546fe2bba5f',panelIndex:'4',type:visualization,version:'7.0.0-alpha1'),(embeddableConfig:(),gridData:(h:11,i:'5',w:48,x:0,y:28),id:'45e07720-b890-11e8-a6d9-e546fe2bba5f',panelIndex:'5',type:visualization,version:'7.0.0-alpha1'),(embeddableConfig:(),gridData:(h:10,i:'6',w:12,x:0,y:18),id:'10f1a240-b891-11e8-a6d9-e546fe2bba5f',panelIndex:'6',type:visualization,version:'7.0.0-alpha1'),(embeddableConfig:(),gridData:(h:11,i:'7',w:12,x:0,y:7),id:b80e6540-b891-11e8-a6d9-e546fe2bba5f,panelIndex:'7',type:visualization,version:'7.0.0-alpha1'),(embeddableConfig:(vis:(colors:('0%20-%2050':%23E24D42,'50%20-%2075':%23EAB839,'75%20-%20100':%237EB26D),defaultColors:('0%20-%2050':'rgb(165,0,38)','50%20-%2075':'rgb(255,255,190)','75%20-%20100':'rgb(0,104,55)'),legendOpen:!f)),gridData:(h:11,i:'8',w:12,x:24,y:7),id:'4b3ec120-b892-11e8-a6d9-e546fe2bba5f',panelIndex:'8',type:visualization,version:'7.0.0-alpha1'),(embeddableConfig:(vis:(colors:('0%20-%202':%23E24D42,'2%20-%203':%23F2C96D,'3%20-%204':%239AC48A),defaultColors:('0%20-%202':'rgb(165,0,38)','2%20-%203':'rgb(255,255,190)','3%20-%204':'rgb(0,104,55)'),legendOpen:!f)),gridData:(h:11,i:'9',w:12,x:36,y:7),id:'9ca7aa90-b892-11e8-a6d9-e546fe2bba5f',panelIndex:'9',type:visualization,version:'7.0.0-alpha1'),(embeddableConfig:(),gridData:(h:18,i:'10',w:48,x:0,y:54),id:'3ba638e0-b894-11e8-a6d9-e546fe2bba5f',panelIndex:'10',type:search,version:'7.0.0-alpha1'),(embeddableConfig:(mapCenter:!(44.29024884874176,0.2977304999999975),mapZoom:2),gridData:(h:15,i:'11',w:24,x:0,y:39),id:'9c6f83f0-bb4d-11e8-9c84-77068524bcab',panelIndex:'11',type:visualization,version:'7.0.0-alpha1'),(embeddableConfig:(),gridData:(h:15,i:'12',w:24,x:24,y:39),id:b72dd430-bb4d-11e8-9c84-77068524bcab,panelIndex:'12',type:visualization,version:'7.0.0-alpha1')),query:(language:lucene,query:''),timeRestore:!t,title:'%5BeCommerce%5D%20Revenue%20Dashboard',viewMode:view)

## Output Elasticsearch query
```json
{
  "bool": {
    "must": [
      {
        "range": {
          "order_date": {
            "gte": "03/15/2019 07:23:58",
            "lte": "03/18/2019 10:43:28"
          }
        }
      },
      {
        "bool": {
          "minimum_should_match": 1,
          "should": [
            {
              "match_phrase": {
                "manufacturer.keyword": "Angeldale"
              }
            },
            {
              "match_phrase": {
                "manufacturer.keyword": "Champion Arts"
              }
            }
          ]
        }
      },
      {
        "match_phrase": {
          "customer_gender": {
            "query": "MALE"
          }
        }
      },
      {
        "match_phrase": {
          "geoip.country_iso_code": {
            "query": "FR"
          }
        }
      }
    ],
    "must_not": []
  }
}
```
